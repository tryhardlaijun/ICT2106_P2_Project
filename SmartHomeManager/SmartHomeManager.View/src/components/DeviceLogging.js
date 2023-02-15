import React, { useState, useEffect } from "react";
import { Button, Select } from "@chakra-ui/react";
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


 * const options = {
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


function DeviceLogging() {
    const [selectDevice, setSelectedDevice] = useState("");
    const [selectDevice2, setSelectedDevice2] = useState("");
    const [device1ID, setDevice1ID] = useState("");
    const [weeklyDevice1Log, setweeklyDevice1Log] = useState([]);
    const [allLog, setAllLog] = useState([]);
    const [test, setTest] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await fetch('http://localhost:5186/api/DeviceLog', {
                    method: 'GET',
                    headers: {
                        accept: 'text/plain',
                    },
                })
                const data = await res.json()
                console.log(data)
                setweeklyDevice1Log(data)
            } catch (error) {
                console.error(error)
            }
        }
        fetchData()
    }, [])


    const onChange = (event) => {
        const value = event.target.value;
        setDevice1ID(value);
    };

    const fetchWeeklyLog = async (id, date) => {
        try {
            //create the fetch request
            const res = await fetch('https://localhost:5186/api/DeviceLog/${id}/${date}', {
                method: 'GET',
                headers: {
                    accept: 'text/plain',
                },
            })

            //get the fetch request
            const data = await res.json()
            setweeklyDevice1Log(data)

        } catch (err) {
            console.error(err)
        }
    }

    // displaying 
    return (
        <div>
            <h2>Device Log</h2>
            <br></br>
            <h2>Select first device</h2>
            <select onChange={onChange} className="form-select">
                <option defaultValue disabled>
                    Select 1st device
                </option>
                <option value="81EB12EA-C3D7-424B-9C5C-EB3681A99E00"> Device 1</option>
                <option value="device2"> Device 2</option>
                <option value="device3"> Device 3</option>
            </select>
            <br></br>

            <Button
                variant="outlined"
                colorScheme='blue'
                onClick={() => {
                    fetchWeeklyLog(device1ID, "2023-02-06 20:02:02.5559728")
                    //fetchDeviceLogs()
                }}
            > View weekly log </Button>
            {weeklyDevice1Log && <h2 className="mt-3">{weeklyDevice1Log}</h2>}

            {device1ID && <h2 className="mt-3">{device1ID}</h2>}

        </div>
    );
};
export default DeviceLogging;