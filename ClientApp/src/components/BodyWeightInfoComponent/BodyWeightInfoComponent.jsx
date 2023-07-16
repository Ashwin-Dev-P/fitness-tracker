import React from "react";

// Components
import BodyWeightFormComponent from "../BodyWeightFormComponent/BodyWeightFormComponent";
import BodyWeightChartComponent from "../BodyWeightChartComponent/BodyWeightChartComponent";

export default function BodyWeightInfoComponent() {
	return (
		<div className="row my-5">
			<div className="col-xs-12 col-lg-9 my-3">
				<BodyWeightChartComponent />
			</div>
			<div className="col-xs-12 col-lg-3 my-3">
				<BodyWeightFormComponent />
			</div>
		</div>
	);
}
