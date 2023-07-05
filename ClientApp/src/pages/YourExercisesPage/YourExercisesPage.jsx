import React from "react";

// Components
import ExercisesDisplayComponent from "../../components/ExercisesDisplayComponent/ExercisesDisplayComponent";
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";
import { useParams } from "react-router-dom";

function YourExercisesPage() {
	const { exercise_plan_id } = useParams();
	const crumbs = [
		{
			name: "Home",
			route: "/",
		},

		{
			name: "Daily plans",
			route: `/your-daily-plans/exercise-plan-id/${exercise_plan_id}`,
		},
		{
			name: "Exercises",
			route: "/exercise-daily-plans",
		},
	];

	return (
		<div>
			<BreadCrumbComponent crumbs={crumbs} />
			<ExercisesDisplayComponent />
		</div>
	);
}
export default YourExercisesPage;
