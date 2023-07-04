import React from "react";

// CSS
import "./ExerciseItemComponent.css";

export default function ExerciseItemComponent(props) {
	const { exerciseId, name, description, imageExtension } = props.exercise;
	return (
		<div className="card">
			<img
				width="100%"
				className="card-img-top"
				src={`https://localhost:7102/assets/images/uploads/exercises/${exerciseId}${imageExtension}`}
				alt={name}
			/>
			<div className="card-body">
				<h3 className="card-title text-center">{name}</h3>
				<p className="card-text">{description}</p>
			</div>
		</div>
	);
}
