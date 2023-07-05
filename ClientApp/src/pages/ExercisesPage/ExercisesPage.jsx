import React from "react";
import { useParams } from "react-router-dom";

// Components
import ExercisesDisplayComponent from "../../components/ExercisesDisplayComponent/ExercisesDisplayComponent";

// Shared components
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";

export default function ExercisesPage() {
	const { exercise_plan_id, exercise_type_id, exercise_daily_plan_id } =
		useParams();

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
			<ExercisesDisplayComponent />
		</div>
	);
}
