import React from "react";
import { Link } from "react-router-dom";

import MyButtonComponent from "../SharedComponents/MyButtonComponent/MyButtonComponent";

export default function ExercisePlanItemComponent(props) {
	const {
		exerciseTypeId,
		exercisePlan,
		planIsSelected,
		AddPlanToMyExercisePlans,
		RemoveExercisePlanFromMyPlans,
	} = props;
	const { name, description, exercisePlanId } = exercisePlan;

	return (
		<div className="shadow p-3 rounded h-100">
			<div>
				<h3>{name}</h3>
				<p>{description}</p>
			</div>
			{planIsSelected ? (
				<div className="row">
					<div className="col-6">
						<Link
							className="btn btn-primary  w-100"
							to={`/your-daily-plans/exercise-plan-id/${exercisePlan.exercisePlanId}`}>
							View
						</Link>
					</div>
					<div className="col-6">
						<MyButtonComponent
							className=" btn-danger  w-100"
							text="Remove"
							onClick={() => {
								RemoveExercisePlanFromMyPlans(exercisePlanId);
							}}
						/>
					</div>
				</div>
			) : (
				<div className="row">
					<div className="col-6">
						<MyButtonComponent
							text="Add"
							className="btn-primary w-100"
							onClick={() => {
								AddPlanToMyExercisePlans(exercisePlanId);
							}}
						/>
					</div>
					<div className="col-6">
						<Link
							to={`/exercise-daily-plans/${exercisePlanId}/exercise-plan/${exerciseTypeId}`}
							className="btn btn-primary w-100">
							View
						</Link>
					</div>
				</div>
			)}
		</div>
	);
}
