import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";
import ExerciseDailyPlanItemComponent from "../../components/ExerciseDailyPlanItemComponent/ExerciseDailyPlanItemComponent";
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";

export default function YourDailyPlansPage() {
	const { exercise_plan_id, exercise_type_id } = useParams();

	const [exerciseDailyPlans, setExerciseDailyPlans] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExerciseDailyPlans(exercise_plan_id);
	}, [exercise_plan_id]);

	const crumbs = [
		{
			name: "Home",
			route: "/",
		},

		{
			name: "Daily plans",
			route: "/exercise-daily-plans",
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
							{exerciseDailyPlans && exerciseDailyPlans.length > 0 ? (
								<>
									<h2 className="text-center">Daily plans</h2>
									<ul className="row">
										{exerciseDailyPlans.map((exerciseDailyPlan) => (
											<li
												key={exerciseDailyPlan.exerciseDailyPlanId}
												className="col-xs-12 col-md-6 col-lg-4 col-xl-3  my-4">
												<Link
													to={`/your-daily-plans/exercise-plan-id/${exercise_plan_id}/exercise-daily-plan-id/${exerciseDailyPlan.exerciseDailyPlanId}`}>
													<ExerciseDailyPlanItemComponent
														exerciseDailyPlan={exerciseDailyPlan}
													/>
												</Link>
											</li>
										))}
									</ul>
								</>
							) : (
								<div>
									<p className="text-center">
										No exercise daily plans available
									</p>
								</div>
							)}
						</>
					)
				) : (
					<div className="text-center">
						<LoadingComponent loading_text="Fetching exercise daily plans..." />
					</div>
				)}
			</div>
		</div>
	);

	async function getExerciseDailyPlans(exercise_type_id) {
		await fetch(`api/exerciseplanmodels/${exercise_type_id}/ExerciseDailyPlans`)
			.then(async (response) => {
				return await response.json();
			})
			.then(async (data) => {
				await setExerciseDailyPlans(data);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise daily plans. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}
