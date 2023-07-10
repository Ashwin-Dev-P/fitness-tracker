import React from "react";

function ExerciseDailyPlanItemComponent(props) {
	const { name, description } = props.exerciseDailyPlan;
	return (
		<div className="shadow p-3 h-100 rounded">
			<h3>{name}</h3>
			<p>{description}</p>
		</div>
	);
}

export default ExerciseDailyPlanItemComponent;
