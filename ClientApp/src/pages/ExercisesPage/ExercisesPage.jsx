import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";
import ExerciseItemComponent from "../../components/ExerciseItemComponent/ExerciseItemComponent";

export default function ExercisesPage() {
	const { exercise_plan_id, exercise_type_id, exercise_daily_plan_id } =
		useParams();

	const [exercises, setExercises] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExercises(exercise_daily_plan_id);
	}, [exercise_daily_plan_id]);

	const crumbs = [
		{
			name: "Home",
			route: "/",
		},
		{
			name: "Exercise types",
			route: "/exercise-type",
		},
		{
			name: "Exercise plans",
			route: `/exercise-plans/${exercise_type_id}`,
		},
		{
			name: "Daily plans",
			route: `/exercise-daily-plans/${exercise_plan_id}/exercise-plan/${exercise_type_id}`,
		},
		{
			name: "Exercises",
			route: `/exercise/${exercise_daily_plan_id}`,
		},
	];

	return (
		<div>
			<BreadCrumbComponent crumbs={crumbs} />
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
