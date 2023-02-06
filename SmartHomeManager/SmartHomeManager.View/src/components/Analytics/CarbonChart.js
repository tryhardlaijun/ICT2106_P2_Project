import React from 'react'
import Chart from "chart.js/auto";
import { Line } from "react-chartjs-2";


const labels = ["January", "February", "March", "April", "May", "June"];

const data = {
  labels: labels,
  datasets: [
    {
      label: "My First dataset",
      backgroundColor: "rgb(255, 99, 132)",
      borderColor: "rgb(255, 99, 132)",
      data: [0, 10, 5, 2, 20, 30, 45],
    },
    {
      label: "My Second dataset",
      backgroundColor: "rgb(124,252,0)",
      borderColor: "rgb(124,252,0)",
      data: [2, 4, 6, 10, 34, 60],
    }
  ],
};

function CarbonChart() {

  return (
    <Line data={data} />
  )
}

export default CarbonChart