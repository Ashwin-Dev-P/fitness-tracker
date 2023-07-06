import React from "react";

export default function MyButtonComponent(props) {
	const { text, onClick, className, type, disabled, loading } = props;
	return (
		<button
			onClick={onClick}
			className={`btn ${className}`}
			type={type}
			disabled={loading || disabled}>
			{loading ? "please wait..." : text}
		</button>
	);
}
