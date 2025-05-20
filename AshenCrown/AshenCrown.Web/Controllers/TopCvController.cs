using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AshenCrown.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopCvController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TopCvController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// GET /api/topcv/summary
        /// Lấy HTML trang chủ https://www.topcv.vn/, rồi parse:
        /// - content của thẻ <title>  
        /// - ví dụ: list các thẻ <h2> (giả sử TopCV có các mục info đánh dấu bằng <h2>)  
        /// Trả về JSON gồm title và danh sách text của từng <h2>.
        /// </summary>
        [HttpGet("summary")]
        public async Task<IActionResult> GetTopCvSummary()
        {
            var client = _httpClientFactory.CreateClient();

            // 1. Thêm header giả lập browser
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/113.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            // 2. Lấy HTML trang chủ TopCV
            var url = "https://www.topcv.vn/";
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, $"Không thể kết nối tới TopCV: {ex.Message}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return StatusCode(403, "TopCV chặn request (403 Forbidden). Có thể cần kiểm tra lại header hoặc IP.");
            }
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Lỗi khi gọi TopCV");
            }

            var html = await response.Content.ReadAsStringAsync();

            // 3. Parse HTML bằng HtmlAgilityPack
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Lấy <title>
            var titleNode = doc.DocumentNode.SelectSingleNode("//title");
            var pageTitle = titleNode != null ? titleNode.InnerText.Trim() : "Không tìm thấy <title>";

            // Lấy tất cả thẻ <h2> (có thể tùy chỉnh XPath nếu muốn)
            var h2Nodes = doc.DocumentNode.SelectNodes("//h2");
            string[] h2Texts;
            if (h2Nodes != null && h2Nodes.Any())
            {
                h2Texts = h2Nodes
                    .Select(n => n.InnerText.Trim())
                    .Where(s => !String.IsNullOrWhiteSpace(s))
                    .ToArray();
            }
            else
            {
                h2Texts = Array.Empty<string>();
            }

            // 4. Trả về JSON
            var result = new
            {
                Title = pageTitle,
                H2Headings = h2Texts
            };

            return Ok(result);
        }

        /// <summary>
        /// GET /api/topcv/job-detail
        /// Fetch và parse chi tiết công việc từ URL đã cho.
        /// </summary>
        [HttpGet("job-detail")]
        public async Task<IActionResult> GetJobDetail()
        {
            var client = _httpClientFactory.CreateClient();

            // 1. Giả lập header để tránh bị chặn
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/113.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            // 2. URL job cụ thể (bạn có thể đưa làm parameter nếu muốn động)
            var jobUrl = "https://www.topcv.vn/tim-viec-lam-lap-trinh-vien-net-tai-ha-noi-kl1?exp=2&type_keyword=1&sba=1&locations=l1";

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(jobUrl);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, $"Không thể kết nối tới TopCV: {ex.Message}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return StatusCode(403, "TopCV chặn request (403 Forbidden).");
            }
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Lỗi khi gọi TopCV");
            }

            var html = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // ==== Bắt đầu parse chi tiết công việc ====

            // 3. Job Title: thường nằm trong <h1> hoặc <h1 class="job-detail-title">
            var titleNode = doc.DocumentNode.SelectSingleNode("//h1");
            var jobTitle = titleNode != null
                ? titleNode.InnerText.Trim()
                : "Không tìm thấy tiêu đề công việc";

            // 4. Company Name: thường nằm trong một <a> hoặc <div class="company-name"> ...
            //    Chúng ta thử tìm <a> có class chứa "company-name"
            var companyNode = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'company-name')]");
            var companyName = companyNode != null
                ? companyNode.InnerText.Trim()
                : "Không tìm thấy tên công ty";

            // 5. Location, Experience, Salary, PostedTime: thường nằm trong một nhóm <span> hoặc <ul class="job-attr">...
            //    Dưới đây chỉ là ví dụ; bạn có thể phải inspect HTML thật để tìm đúng selector.
            string location = "Không tìm thấy";
            string experience = "Không tìm thấy";
            string salary = "Không tìm thấy";
            string postedTime = "Không tìm thấy";

            // Ví dụ: giả sử các thẻ info nằm trong <ul class="job-attr"> với từng <li>
            var attrNodes = doc.DocumentNode.SelectNodes("//ul[contains(@class,'job-attr')]//li");
            if (attrNodes != null)
            {
                foreach (var li in attrNodes)
                {
                    var text = li.InnerText.Trim();
                    // Giả sử li chứa "Địa điểm: Hà Nội", "Kinh nghiệm: Dưới 1 năm", "Mức lương: Tới 3 triệu", "Đăng: 3 tuần trước"
                    if (text.StartsWith("Địa điểm") || text.StartsWith("Hà Nội") || text.Contains("Hà Nội"))
                    {
                        location = text;
                    }
                    else if (text.StartsWith("Kinh nghiệm") || text.Contains("Kinh nghiệm"))
                    {
                        experience = text;
                    }
                    else if (text.StartsWith("Mức lương") || text.Contains("Mức lương"))
                    {
                        salary = text;
                    }
                    else if (text.StartsWith("Đăng") || text.Contains("trước"))
                    {
                        postedTime = text;
                    }
                }
            }

            // 6. Job Description: thường nằm trong một <div class="job-description"> hoặc <div id="job-description">
            //    Chúng ta thử chọn toàn bộ nội dung trong phần mô tả này.
            var descNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'job-description') or contains(@id,'job-description')]");
            var jobDescription = descNode != null
                ? descNode.InnerText.Trim()
                : "Không tìm thấy phần mô tả công việc";

            // 7. Trả về JSON
            var result = new
            {
                JobTitle = jobTitle,
                CompanyName = companyName,
                Location = location,
                Experience = experience,
                Salary = salary,
                PostedTime = postedTime,
                JobDescription = jobDescription
            };

            return Ok(result);
        }
    }
}
