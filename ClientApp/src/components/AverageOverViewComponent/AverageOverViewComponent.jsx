import React from "react";
import AverageSleepComponent from "../AverageSleepComponent/AverageSleepComponent";

export default function AverageOverViewComponent() {
	return (
		<div>
			<div className="row my-5">
				<div className="col-6  col-md-3  ">
					<AverageSleepComponent />
				</div>
			</div>
		</div>
	);
}
