using Microsoft.AspNetCore.SignalR;

namespace AshenCrown.Web.Hub2
{
    public class CountHub : Hub
    {
        private static int _count = 0;

        public Task<int> GetCount()
        {
            return Task.FromResult(_count);
        }

        public async Task CountGaming()
        {
            _count++;
            await Clients.All.SendAsync("loadCount", _count);
        }
    }


}
