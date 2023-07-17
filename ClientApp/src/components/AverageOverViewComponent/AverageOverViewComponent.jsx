import React from "react";
import AverageSleepComponent from "../AverageSleepComponent/AverageSleepComponent";
import AverageCalorieComponent from "../AverageCalorieComponent/AverageCalorieComponent";
import FavouriteExerciseComponent from "../FavouriteExerciseComponent/FavouriteExerciseComponent";

export default function AverageOverViewComponent() {
	return (
		<div>
			<div className="row my-5">
				<div className="col-6  col-md-3  my-3">
					<AverageSleepComponent />
				</div>
				<div className="col-6  col-md-3  my-3">
					<AverageCalorieComponent />
				</div>
				<div className="col-6  col-md-3  my-3">
					<FavouriteExerciseComponent />
				</div>
			</div>
		</div>
	);
}
