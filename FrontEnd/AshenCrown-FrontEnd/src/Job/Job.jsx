import React, { useEffect, useState } from "react";
import api from "../API/api";
import axios from "axios";
const Job = () => {
  const [jobs, setJobs] = useState([]);
  useEffect(() => {
    axios
      .get("https://localhost:7296/api/jobs")
      .then((response) => {
        console.log(response.data);
        setJobs(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  return (
    <div>
      <h2>Danh sách việc làm</h2>
      <ul>
        {jobs.map((job) => (
          <li key={job.id}>
            <strong>{job.title}</strong> tại <em>{job.company}</em> (
            {job.location})
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Job;
