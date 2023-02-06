import React from 'react'
import Chart from "chart.js/auto";
import { Line } from "react-chartjs-2";
import { getByPlaceholderText } from '@testing-library/react';


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
    },
  ],
};

const options = {
  legend:{
    labels:{
      font:{
        size:30,
      }
    }
  },
  scales: {
      x: {
          beginAtZero: true,
          title:{
              display: true,
              text: "Month",
              font:{
                size: 30,
                weight: "bold",
              }
          },
          ticks: {
              font: {
                  size: 30,
              },
          },
      },
      y:{
        beginAtZero: true,
        title:{
          display: true,
          text: "Carbon Footprint",
          font:{
            size: 30,
            weight: "bold",
          }
      },
        ticks: {
          font: {
              size: 30,
          }
      }
      },

  }
};



function CarbonChart() {

  return (
    <Line options={options} data={data} />
  )
}

export default CarbonChart