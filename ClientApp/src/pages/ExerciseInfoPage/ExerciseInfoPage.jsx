import React, { useEffect, useState } from "react";

import { useParams } from "react-router-dom";

// Service
import authService from "../../components/api-authorization/AuthorizeService";

// Components
import ExerciseItemComponent from "../../components/ExerciseItemComponent/ExerciseItemComponent";
import ExerciseIntensityComponent from "../../components/ExerciseIntensityComponent/ExerciseIntensityComponent";
import ExerciseProgressChartComponent from "../../components/ExerciseProgressChartComponent/ExerciseProgressChartComponent";

// Shared components
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";

export default function ExerciseInfoPage() {
	// Check if user is loggedIn
	const [isLoggedIn, setIsLoggedIn] = useState(false);
	const promise1 = Promise.resolve(authService.isAuthenticated());
	promise1.then((loginStatus) => {
		setIsLoggedIn(loginStatus);
	});

	const { exercise_id } = useParams();

	const [exercise, setExercise] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExerciseInfo(exercise_id);
	}, [exercise_id]);
	return (
		<div className="mx-3">
			<h2 className="text-center">Exercise info</h2>
			<div>
				{loading === false ? (
					errorMessage ? (
						<div className="text-center text-danger">{errorMessage}</div>
					) : (
						<>
							<div className="row mt-3">
								<div className="col-xs-12 col-md-6 col-lg-4 col-xl-3 mt-3">
									<div className="row">
										<ExerciseItemComponent exercise={exercise} />
									</div>
									<div className="row">
										<ExerciseIntensityComponent
											exerciseId={exercise_id}
											isLoggedIn={isLoggedIn}
										/>
									</div>
								</div>
								<div className="col-xs-12 col-md-6 col-lg-8 col-xl-9 my-3 px-0 px-md-3 text-center">
									<ExerciseProgressChartComponent
										exerciseId={exercise_id}
										isLoggedIn={isLoggedIn}
									/>
								</div>
							</div>
						</>
					)
				) : (
					<div className="text-center">
						<LoadingComponent loading_text="Loading exercises..." />
					</div>
				)}
			</div>
		</div>
	);

	async function getExerciseInfo(exercise_id) {
		await fetch(`api/ExerciseModels/${exercise_id}`)
			.then(async (response) => {
				return await response.json();
			})
			.then(async (data) => {
				await setExercise(data);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise info. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}
