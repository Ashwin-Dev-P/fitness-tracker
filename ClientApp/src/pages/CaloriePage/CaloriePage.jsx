import React from "react";

// Components

// Shared components
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";
import CalorieChartComponent from "../../components/CalorieChartComponent/CalorieChartComponent";
import CalorieFormComponent from "../../components/CalorieFormComponent/CalorieFormComponent";

function CaloriePage() {
	const crumbs = [
		{
			name: "Home",
			route: "/",
		},
		{
			name: "Calorie",
			route: "/calorie",
		},
	];

	return (
		<div>
			<div className="row">
				<BreadCrumbComponent crumbs={crumbs} />
			</div>
			<div className="row min-vh-80 d-flex align-items-center justify-content-center">
				<div className="col-xs-12 col-lg-3">
					<CalorieFormComponent />
				</div>
				<div className="col-xs-12 col-lg-9">
					<CalorieChartComponent />
				</div>
			</div>
		</div>
	);
}
export default CaloriePage;
