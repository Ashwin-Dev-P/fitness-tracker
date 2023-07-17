import React, { useEffect, useState } from "react";
import authService from "../api-authorization/AuthorizeService";

import CircularProgressBarComponent from "../SharedComponents/CircularProgressBarComponent/CircularProgressBarComponent";
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";
import { Link } from "react-router-dom";

function AverageSleepComponent() {
	const [averageSleep, setAverageSleep] = useState(null);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getAverageSleep();
	}, []);

	return (
		<>
			<div className="card p-4 shadow">
				<h4 className="mb-3">Average sleep</h4>

				{loading ? (
					<LoadingComponent />
				) : (
					<>
						{errorMessage ? (
							<div className="text-center text-danger">{errorMessage}</div>
						) : (
							<>
								<CircularProgressBarComponent
									progressValue={averageSleep}
									progressText={`${averageSleep} hrs`}
									maxValue={9.5}
								/>
								<Link
									to="sleep"
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

	async function getAverageSleep(exercise_daily_plan_id) {
		const token = await authService.getAccessToken();
		await fetch(`api/SleepModels/mySleep/Average`, {
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
					setAverageSleep(0);
					return;
				}
				setAverageSleep((responseObj / 60).toFixed(2));
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

export default AverageSleepComponent;
