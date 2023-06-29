import React from "react";

export default function LoadingComponent(props) {
	var { loading_text } = props;

	if (!loading_text) {
		loading_text = "Loading...";
	}

	return <span>{loading_text}</span>;
}
