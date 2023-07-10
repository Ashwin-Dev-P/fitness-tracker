import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

// Components
import ExercisePlanItemComponent from "../../components/ExercisePlanItemComponent/ExercisePlanItemComponent";

// Shared components
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";
import authService from "../../components/api-authorization/AuthorizeService";

function ExercisePlansPage() {
	const { exercise_type_id } = useParams();

	const [exercisePlans, setExercisePlans] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExercisePlans(exercise_type_id);
	}, [exercise_type_id]);

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
			route: "/exercise-plans",
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
												<ExercisePlanItemComponent
													exercisePlan={exercisePlan}
													exerciseTypeId={exercise_type_id}
													planIsSelected={false}
													AddPlanToMyExercisePlans={AddPlanToMyExercisePlans}
												/>
											</li>
										))}
									</ul>
								</>
							) : (
								<div className="min-vh-80 d-flex h-100 justify-content-center align-items-center">
									<p className="text-center ">No exercise plans available</p>
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

	async function getExercisePlans(exercise_type_id) {
		const token = await authService.getAccessToken();
		await fetch(`api/ExerciseTypeModels/${exercise_type_id}`, {
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
			.then(async (data) => {
				await setExercisePlans(data);
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

	async function AddPlanToMyExercisePlans(exercisePlanId) {
		const token = await authService.getAccessToken();
		await fetch(`api/ApplicationUserExercisePlanModels`, {
			method: "POST",
			body: JSON.stringify({
				ExercisePlanId: exercisePlanId,
			}),
			headers: !token
				? {}
				: {
						Authorization: `Bearer ${token}`,
						"Content-type": "application/json; charset=UTF-8",
				  },
		})
			.then(() => {
				setExercisePlans(
					exercisePlans.filter(
						(exercisePlan) => exercisePlan.exercisePlanId != exercisePlanId
					)
				);
			})

			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to add exercise plan. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});
	}
}

export default ExercisePlansPage;
