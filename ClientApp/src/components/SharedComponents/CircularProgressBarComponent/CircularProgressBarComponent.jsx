import React from "react";

import { CircularProgressbar } from "react-circular-progressbar";
import "react-circular-progressbar/dist/styles.css";

export default function CircularProgressBarComponent(props) {
	const { progressValue, progressText, maxValue } = props;
	return (
		<CircularProgressbar
			value={progressValue}
			text={progressText}
			maxValue={maxValue}
		/>
	);
}
