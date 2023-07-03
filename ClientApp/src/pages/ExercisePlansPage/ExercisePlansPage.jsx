import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";
import ExercisePlanItemComponent from "../../components/ExercisePlanItemComponent/ExercisePlanItemComponent";

export default function ExercisePlansPage() {
	const { exercise_type_id } = useParams();
	console.log(exercise_type_id);

	const [exercisePlans, setExercisePlans] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExercisePlans(exercise_type_id);
	}, []);

	return (
		<div>
			<div>
				{loading === false ? (
					errorMessage ? (
						<div className="text-center text-danger">{errorMessage}</div>
					) : (
						<>
							{exercisePlans && exercisePlans.length > 0 ? (
								<>
									<h2 className="text-center">
										Select your desired training plan
									</h2>
									<ul className="row">
										{exercisePlans.map((exercisePlan) => (
											<li
												key={exercisePlan.exercisePlanId}
												className="col-xs-12 col-md-6 col-lg-4 col-xl-3  my-4">
												<Link
													to={`/exercise-daily-plans/${exercisePlan.exercisePlanId}`}>
													<ExercisePlanItemComponent
														exercisePlan={exercisePlan}
													/>
												</Link>
											</li>
										))}
									</ul>
								</>
							) : (
								<div>
									<p>No exercise plans available</p>
								</div>
							)}
						</>
					)
				) : (
					<div className="text-center">
						<LoadingComponent loading_text="Fetching exercise plans..." />
					</div>
				)}
			</div>
		</div>
	);

	function getExercisePlans(exercise_type_id) {
		fetch(`api/ExerciseTypeModels/${exercise_type_id}`)
			.then((response) => {
				return response.json();
			})
			.then((data) => {
				console.log(data);
				setExercisePlans(data);
			})
			.catch((error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise types. Something went wrong";

				setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		setLoading(false);
	}
}
