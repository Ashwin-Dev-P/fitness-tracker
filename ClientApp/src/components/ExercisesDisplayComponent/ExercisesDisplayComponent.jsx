import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

import ExerciseItemComponent from "../ExerciseItemComponent/ExerciseItemComponent";
import LoadingComponent from "../SharedComponents/LoadingComponent/LoadingComponent";

function ExercisesDisplayComponent() {
	const { exercise_plan_id, exercise_type_id, exercise_daily_plan_id } =
		useParams();

	const [exercises, setExercises] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExercises(exercise_daily_plan_id);
	}, [exercise_daily_plan_id]);

	return (
		<div>
			<div>
				{loading === false ? (
					errorMessage ? (
						<div className="text-center text-danger">{errorMessage}</div>
					) : (
						<>
							{exercises && exercises.length > 0 ? (
								<>
									<h2 className="text-center">Exercises</h2>
									<ul className="row">
										{exercises.map((exercise) => (
											<li
												key={exercise.exerciseId}
												className="col-xs-12 col-md-6 col-lg-4 col-xl-3  my-4">
												<Link
													to={`/exercises/exercise-id/${exercise.exerciseId}`}>
													<ExerciseItemComponent exercise={exercise} />
												</Link>
											</li>
										))}
									</ul>
								</>
							) : (
								<div>
									<p className="text-center">No exercise available</p>
								</div>
							)}
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

	async function getExercises(exercise_daily_plan_id) {
		await fetch(
			`api/exercisedailyplanmodels/${exercise_daily_plan_id}/exercises`
		)
			.then(async (response) => {
				return await response.json();
			})
			.then(async (data) => {
				await setExercises(data);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercises. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}
export default ExercisesDisplayComponent;
