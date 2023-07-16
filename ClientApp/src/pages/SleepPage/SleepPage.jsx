import React from "react";

// Components
import SleepFormComponent from "../../components/SleepFormComponent/SleepFormComponent";
import SleepChartComponent from "../../components/SleepChartComponent/SleepChartComponent";

// Shared components
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";

export default function SleepPage() {
	const crumbs = [
		{
			name: "Home",
			route: "/",
		},
		{
			name: "Sleep",
			route: "/sleep",
		},
	];

	return (
		<div>
			<div className="row">
				<BreadCrumbComponent crumbs={crumbs} />
			</div>
			<div className="row min-vh-80 d-flex align-items-center justify-content-center">
				<div className="col-xs-12 col-lg-3">
					<SleepFormComponent />
				</div>
				<div className="col-xs-12 col-lg-9">
					<SleepChartComponent />
				</div>
			</div>
		</div>
	);
}
