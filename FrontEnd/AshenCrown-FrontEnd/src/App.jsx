import { use, useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import Job from "./Job/Job";
import Test from "./Job/Test";
import { useEffect } from "react";
import axios from "axios";
function App() {
  return (
    <div>
      <Job />
    </div>
  );
}

export default App;
