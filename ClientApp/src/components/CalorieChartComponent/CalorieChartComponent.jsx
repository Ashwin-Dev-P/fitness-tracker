import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import { Line } from "react-chartjs-2";

// Service
import authService from "../api-authorization/AuthorizeService";
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";

const options = {
	responsive: true,
	plugins: {
		legend: {
			position: "top",
		},
		title: {
			display: true,
			text: "Calorie intake progress",
		},
	},
};

const months = [
	"Jan",
	"Feb",
	"Mar",
	"Apr",
	"May",
	"Jun",
	"Jul",
	"Aug",
	"Sept",
	"Oct",
	"Nov",
	"Dec",
];

function CalorieChartComponent() {
	// Check if user is loggedIn
	const isLoggedIn = true;

	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	const [timeLineLabels, setTimeLineLabels] = useState([]);
	const [calorieCountData, setCalorieData] = useState([]);

	//const weightProgressData = [10, 12, 2121, 2121];

	const data = {
		labels: timeLineLabels,
		datasets: [
			{
				label: "calorie",
				data: calorieCountData,
				borderColor: "rgb(255, 99, 132)",
				backgroundColor: "rgba(255, 99, 132, 0.5)",
				tension: 0.2,
				indexAxis: "x",
			},
		],
	};

	useEffect(() => {
		//if (isLoggedIn) {
		getSleepData();
		//}
	}, []);

	return (
		<div className="card shadow p-xs-1 p-md-3">
			<div className="d-flex h-100 justify-content-center align-items-center">
				{isLoggedIn ? (
					<>
						{loading ? (
							<LoadingComponent />
						) : (
							<>
								{errorMessage ? (
									<p className="text-danger">{errorMessage}</p>
								) : (
									<>
										{calorieCountData && calorieCountData.length === 0 ? (
											<p className="p-1">
												No data available. Add sleep duration to see your
												progress as graph
											</p>
										) : (
											<Line
												options={options}
												data={data}
											/>
										)}
									</>
								)}
							</>
						)}
					</>
				) : (
					<div>
						<p>Login to get analysis of your progress as graph.</p>
						<Link
							to="/authentication/login"
							className="btn btn-primary">
							Login
						</Link>
					</div>
				)}
			</div>
		</div>
	);

	async function convertDateTimeToDate(dateTime) {
		const dateObj = new Date(dateTime.split("T")[0]);

		const date = dateObj.getDate();
		const month = dateObj.getMonth();
		const year = dateObj.getFullYear();

		return `${months[month]} ${date}, ${year}`;
	}

	async function getHoursFromDuration(duration) {
		const durationArr = duration.split(":");
		const hours = Number(durationArr[0]);
		const minutes = Number(durationArr[1]);
		console.log(durationArr, hours, minutes, hours + minutes / 60);
		return hours + minutes / 60;
	}

	async function getSleepData() {
		const token = await authService.getAccessToken();
		fetch(`api/CalorieModels/MyCalorie`, {
			headers: !token
				? {}
				: {
						Authorization: `Bearer ${token}`,
						"Content-type": "application/json; charset=UTF-8",
				  },
		})
			.then(async (response) => {
				if (response.status === 200) {
					return await response.json();
				} else {
					throw new Error("Unable to get graph data. Something went wrong");
				}
			})

			.then(async (calorieData) => {
				console.log(calorieData);

				let calorieCountArray = [];
				let dateConversionPromiseArray = [];
				for (const calorieDatum of calorieData) {
					const { calorieConsumptionDate, calorieCount } = calorieDatum;
					//const { sleepDuration, sleepDate } = sleepDatum;

					// Push all dateConversion function promises inside array which will be resolved concurrently later
					dateConversionPromiseArray.push(
						convertDateTimeToDate(calorieConsumptionDate)
					);
					calorieCountArray.push(calorieCount);
				}

				// Using promise all to concurrently convert all sql date time to readable date.
				// Concurrently running is more efficient
				Promise.all(dateConversionPromiseArray).then(async (dateArray) => {
					setTimeLineLabels(dateArray);
				});

				setCalorieData(calorieCountArray);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to get data. Something went wrong";

				setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		setLoading(false);
	}
}

export default CalorieChartComponent;
