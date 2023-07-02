import React from "react";

// CSS
import "./ExerciseTypeItemComponent.css";

export default function ExerciseTypeItemComponent(props) {
	const { exercise_type } = props;
	const { exerciseTypeId, name, description, imageExtension } = exercise_type;
	return (
		<div className="card">
			<img
				width="100%"
				className="card-img-top"
				src={`https://localhost:7102/assets/images/uploads/exercise_types/${exerciseTypeId}${imageExtension}`}
				alt="Card image cap"
			/>
			<div className="card-body">
				<h3 className="card-title text-center">{name}</h3>
				<p className="card-text">{description}</p>
			</div>
		</div>
	);
}
