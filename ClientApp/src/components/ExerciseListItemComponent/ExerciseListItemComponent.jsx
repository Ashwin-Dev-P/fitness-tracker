import React from "react";

function ExerciseListItemComponent(props) {
	const { exerciseName } = props;

	return (
		<div className="card p-2 shadow-sm">
			<span className="card-title text-center">{exerciseName}</span>
		</div>
	);
}
export default ExerciseListItemComponent;
