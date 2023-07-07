import React, { useEffect, useState } from "react";
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
//import faker from "faker";

// Sercies
import authService from "../api-authorization/AuthorizeService";
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

function ExerciseProgressChartComponent(props) {
	const { exerciseId } = props;

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
		getExerciseIntensity(exerciseId);
	}, [exerciseId]);

	return (
		<div className="card shadow-sm">
			{loading ? (
				<LoadingComponent />
			) : (
				<>
					<div className="d-flex h-100 justify-content-center align-items-center">
						{errorMessage ? (
							<p className="text-danger">{errorMessage}</p>
						) : (
							<>
								{weightProgressData && weightProgressData.length === 0 ? (
									<p>
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
					</div>
				</>
			)}
		</div>
	);

	async function getExerciseIntensity(exerciseId) {
		const token = await authService.getAccessToken();
		await fetch(`api/Intensitymodels/Exercise/${exerciseId}`, {
			headers: !token
				? {}
				: {
						Authorization: `Bearer ${token}`,
						"Content-type": "application/json; charset=UTF-8",
				  },
		})
			.then(async (response) => {
				return await response.json();
			})
			.then(async (intensityData) => {
				let weightsArray = [];
				let dateArray = [];

				for (const intensityDatum of intensityData) {
					weightsArray.push(intensityDatum.weights);
					dateArray.push(intensityDatum.exerciseDate);
				}
				await setTimeLineLabels(dateArray);
				await setWeightProgressData(weightsArray);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise plans. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}

export default ExerciseProgressChartComponent;
