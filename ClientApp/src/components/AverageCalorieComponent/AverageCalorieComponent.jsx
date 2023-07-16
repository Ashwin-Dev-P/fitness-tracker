import React, { useEffect, useState } from "react";
import authService from "../api-authorization/AuthorizeService";

import CircularProgressBarComponent from "../SharedComponents/CircularProgressBarComponent/CircularProgressBarComponent";
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";
import { Link } from "react-router-dom";

function AverageCalorieComponent() {
	const [averageCalorie, setAverageCalorie] = useState(null);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getAverageCalorie();
	}, []);

	return (
		<>
			<div className="card p-4 shadow">
				{loading ? (
					<LoadingComponent />
				) : (
					<>
						{errorMessage ? (
							<div className="text-center text-danger">{errorMessage}</div>
						) : (
							<>
								<h4 className="mb-3">Avg calorie</h4>
								<CircularProgressBarComponent
									progressValue={averageCalorie}
									progressText={`${averageCalorie}`}
									maxValue={3000}
								/>
								<Link
									to="calorie"
									className="btn btn-primary mt-4">
									View
								</Link>
							</>
						)}
					</>
				)}
			</div>
		</>
	);

	async function getAverageCalorie(exercise_daily_plan_id) {
		const token = await authService.getAccessToken();
		await fetch(`api/CalorieModels/MyCalorie/Average`, {
			method: "GET",

			headers: !token
				? {}
				: {
						Authorization: `Bearer ${token}`,
						"Content-type": "application/json; charset=UTF-8",
				  },
		})
			.then(async (response) => {
				const { status } = response;
				if (status === 200) {
					console.log(response);
					return response.json();
				} else if (status === 404) {
					return 0;
				} else if (status === 401) {
					throw new Error("Unauthorized. Login in to continue");
				} else {
					throw new Error("Unable to get sleep data");
				}
			})
			.then((responseObj) => {
				if (responseObj === 0) {
					setAverageCalorie(0);
					return;
				}
				setAverageCalorie(responseObj.toFixed(2));
			})

			.catch(async (error) => {
				const error_message =
					error && error.message ? error.message : "Something went wrong";

				setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}

export default AverageCalorieComponent;
