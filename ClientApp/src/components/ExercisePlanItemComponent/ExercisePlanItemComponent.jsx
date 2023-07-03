import React from "react";

export default function ExercisePlanItemComponent(props) {
	const { name, description, exercisePlanId } = props.exercisePlan;
	return (
		<div className="shadow p-3">
			<h3>{name}</h3>
			<p>{description}</p>
		</div>
	);
}
