import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import {
	Chart as ChartJS,
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	Title,
	Tooltip,
	Legend,
} from "chart.js";
import { Line } from "react-chartjs-2";

// Service
import authService from "../api-authorization/AuthorizeService";

// Components
// Shared components
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";

ChartJS.register(
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
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
			text: "Your progress",
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

function ExerciseProgressChartComponent(props) {
	const { exerciseId } = props;

	// Check if user is loggedIn
	const { isLoggedIn } = props;

	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	const [timeLineLabels, setTimeLineLabels] = useState([]);
	const [weightProgressData, setWeightProgressData] = useState([]);

	//const weightProgressData = [10, 12, 2121, 2121];

	const data = {
		labels: timeLineLabels,
		datasets: [
			{
				label: "weight in kg",
				data: weightProgressData,
				borderColor: "rgb(255, 99, 132)",
				backgroundColor: "rgba(255, 99, 132, 0.5)",
				tension: 0.2,
				indexAxis: "x",
			},
		],
	};

	useEffect(() => {
		if (isLoggedIn) {
			getExerciseIntensity(exerciseId);
		}
	}, [exerciseId, isLoggedIn]);

	return (
		<div className="card shadow-sm">
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
										{weightProgressData && weightProgressData.length === 0 ? (
											<p className="p-1">
												No data available. Add intensity to see your progress as
												graph
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

	async function getExerciseIntensity(exerciseId) {
		const token = await authService.getAccessToken();
		fetch(`api/Intensitymodels/Exercise/${exerciseId}`, {
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

			.then(async (intensityData) => {
				console.log(intensityData);
				let weightsArray = [];

				let dateConversionPromiseArray = [];
				for (const intensityDatum of intensityData) {
					weightsArray.push(intensityDatum.weights);

					// Push all dateConversion function promises inside array which will be resolved concurrently later
					dateConversionPromiseArray.push(
						convertDateTimeToDate(intensityDatum.exerciseDate)
					);
				}

				// Using promise all to concurrently convert all sql date time to readable date.
				// Concurrently running is more efficient
				Promise.all(dateConversionPromiseArray).then(async (dateArray) => {
					setTimeLineLabels(dateArray);
				});

				setWeightProgressData(weightsArray);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise plans. Something went wrong";

				setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		setLoading(false);
	}
}

export default ExerciseProgressChartComponent;
