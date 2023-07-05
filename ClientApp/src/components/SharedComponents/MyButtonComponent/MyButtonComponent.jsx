import React from "react";

export default function MyButtonComponent(props) {
	const { text, onClick, className } = props;
	return (
		<button
			onClick={onClick}
			className={`btn ${className}`}>
			{text}
		</button>
	);
}
