import React, { useEffect, useState } from "react";
import ExerciseItemComponent from "../ExerciseItemComponent/ExerciseItemComponent";
import authService from "../api-authorization/AuthorizeService";
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";

export default function FavouriteExerciseComponent() {
	const [exercise, setExercise] = useState(null);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getAverageSleep();
	}, []);

	return (
		<div className="card">
			<h4 className="text-center p-2"> Favourite exercise</h4>
			<div className="card d-flex align-items-center justify-content-center  mt-md-0">
				{loading ? (
					<LoadingComponent />
				) : (
					<>
						{errorMessage ? (
							<div className="text-center text-danger">{errorMessage}</div>
						) : (
							<>
								<ExerciseItemComponent exercise={exercise} />
							</>
						)}
					</>
				)}
			</div>
		</div>
	);

	async function getAverageSleep(exercise_daily_plan_id) {
		const token = await authService.getAccessToken();
		await fetch(`api/IntensityModels/FavouriteExercise`, {
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
					throw new Error(
						"Start adding exercise intensity to track your favourite exercise"
					);
				} else if (status === 401) {
					throw new Error("Unauthorized. Login in to continue");
				} else {
					throw new Error("Unable to get exercise data");
				}
			})
			.then((responseObj) => {
				setExercise(responseObj);
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
