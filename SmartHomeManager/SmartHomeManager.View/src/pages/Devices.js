import React, { useState } from "react";
import { Select } from "@chakra-ui/react";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);
const options = {
    responsive: true,
    plugins: {
        legend: {
            position: "top",
        },
        title: {
            display: true,
            text: "Device Energy Usage Graph",
        },
    },
};
const options2 = {
    responsive: true,
    plugins: {
        legend: {
            position: "top",
        },
        title: {
            display: true,
            text: "Device Activity Graph",
        },
    },
};
const weeklyLabel = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

const hourlyLabel = [
    "00:00",
    "01:00",
    "02:00",
    "03:00",
    "04:00",
    "05:00",
    "06:00",
    "07:00",
    "08:00",
    "09:00",
    "10:00",
    "11:00",
    "12:00",
    "13:00",
    "14:00",
    "15:00",
    "16:00",
    "17:00",
    "18:00",
    "19:00",
    "20:00",
    "21:00",
    "22:00",
    "23:00",
];

const weeklyData = {
    labels: weeklyLabel,
    datasets: [
        {
            label: "Device 1",
            data: [10, 20, 30, 40, 50, 60, 70],
            backgroundColor: "rgba(255, 99, 132, 0.5)",
        },
        {
            label: "Device 2",
            data: [30, 40, 50, 60, 70, 80, 92],
            backgroundColor: "rgba(53, 162, 235, 0.5)",
        },
    ],
};

const hourlyData = {
    labels: hourlyLabel,
    datasets: [
        {
            label: "Device 1",
            data: [
                15, 35, 39, 92, 77, 36, 2, 25, 45, 49, 102, 87, 46, 12, 65, 85, 89, 52,
                67, 26, 21, 22, 65, 24,
            ],
            backgroundColor: "rgba(255, 99, 132, 0.5)",
        },
        {
            label: "Device 2",
            data: [
                24, 65, 22, 21, 26, 67, 52, 89, 85, 65, 12, 46, 87, 102, 49, 45, 25, 2,
                36, 77, 92, 39, 35, 15,
            ],
            backgroundColor: "rgba(53, 162, 235, 0.5)",
        },
    ],
};

const weeklyActivityData = {
    labels: weeklyLabel,
    datasets: [
        {
            label: "Device 1",
            data: [50, 20, 30, 10, 50, 20, 60],
            backgroundColor: "rgba(255, 99, 132, 0.5)",
        },
        {
            label: "Device 2",
            data: [30, 40, 50, 60, 70, 80, 52],
            backgroundColor: "rgba(53, 162, 235, 0.5)",
        },
    ],
};

const hourlyActivityData = {
    labels: hourlyLabel,
    datasets: [
        {
            label: "Device 1",
            data: [
                15, 35, 39, 92, 77, 36, 2, 25, 45, 49, 102, 87, 46, 12, 65, 85, 89, 52,
                67, 26, 21, 22, 65, 24,
            ],
            backgroundColor: "rgba(255, 99, 132, 0.5)",
        },
        {
            label: "Device 2",
            data: [
                80, 65, 22, 21, 26, 67, 52, 19, 25, 65, 12, 46, 87, 70, 49, 45, 25, 2,
                36, 77, 92, 39, 35, 15,
            ],
            backgroundColor: "rgba(53, 162, 235, 0.5)",
        },
    ],
};


export default function Devices() {
    const [DropDownValue, setDropDownValue] = useState(null);
    const [DropDownValue2, setDropDownValue2] = useState(null);
    const [chartData, setChartData] = useState(weeklyData);
    const [chartTwoData, setChartTwoData] = useState(weeklyActivityData);

    const dropDownHandler = (e) => {
        let value = e.target.value;
        setDropDownValue(value);
        if (value === "option1") {
            setChartData(weeklyData);
        } else if (value === "option2") {
            setChartData(hourlyData);
        }
    };

    const dropDownHandler2 = (e) => {
        let value = e.target.value;
        setDropDownValue2(value);
        if (value === "weekly") {
            setChartTwoData(weeklyActivityData);
        } else if (value === "hourly") {
            setChartTwoData(hourlyActivityData);
        }
    };

    return (
        <div>
            <strong>Graphs</strong><br></br><br></br>
            <Select
                placeholder="Select Device Energy Usage View"
                onChange={(e) => dropDownHandler(e)}
                width={"200px"}
            >
                <option value="option1">Weekly</option>
                <option value="option2">Hourly</option>
            </Select>
            {DropDownValue ? (
                <div style={{ width: "1300px" }}>
                    <Bar options={options} data={chartData} />
                </div>
            ) : (
                <div>
                    <strong>Please select any value from dropdown</strong>
                </div>
            )}
            <br></br>
            <br></br>
            <Select
                placeholder="Select Device Activity View"
                onChange={(e) => dropDownHandler2(e)}
                width={"200px"}
            >
                <option value="weekly">Weekly</option>
                <option value="hourly">Hourly</option>
            </Select>
            {DropDownValue2 ? (
                <div style={{ width: "1300px" }}>
                    <Bar options={options2} data={chartTwoData} />
                </div>
            ) : (
                <div>
                    <strong>Please select any value from dropdown</strong>
                </div>
            )}
        </div>
    );
}