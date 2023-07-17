import React, { Component } from "react";
import { Link } from "react-router-dom";

// Sercies
import authService from "./api-authorization/AuthorizeService";

// Components
import ExercisePlanItemComponent from "./ExercisePlanItemComponent/ExercisePlanItemComponent";

// Shared components
import LoadingComponent from "./SharedComponents/LoadingComponent/LoadingComponent";
import BodyWeightChartComponent from "./BodyWeightChartComponent/BodyWeightChartComponent";
import BodyWeightInfoComponent from "./BodyWeightInfoComponent/BodyWeightInfoComponent";
import AverageOverViewComponent from "./AverageOverViewComponent/AverageOverViewComponent";

export class Home extends Component {
	constructor(props) {
		super(props);

		this.state = {
			exercisePlans: null,
			loading: true,
			errorMessage: null,
		};

		this.removeExercisePlans = this.removeExercisePlans.bind(this);
	}
	componentDidMount() {
		this.getExercisePlans();
	}

	render() {
		var { loading, errorMessage, exercisePlans } = this.state;
		return (
			<div>
				<h2 className="text-center">Dashboard</h2>
				<hr />
				<div>
					{loading === false ? (
						errorMessage ? (
							<div className="text-center text-danger">{errorMessage}</div>
						) : (
							<>
								{exercisePlans && exercisePlans.length > 0 ? (
									<>
										<h2 className="text-center">Your exercise plans</h2>
										<ul className="row">
											{exercisePlans.map((exercisePlan) => (
												<li
													key={exercisePlan.exercisePlanId}
													className="col-xs-12 col-md-6 col-lg-4 col-xl-3  my-4">
													<ExercisePlanItemComponent
														exercisePlan={exercisePlan}
														planIsSelected={true}
														RemoveExercisePlanFromMyPlans={
															this.removeExercisePlans
														}
													/>
												</li>
											))}
										</ul>
									</>
								) : (
									<div className=" text-center mt-5">
										<p className="text-center">You don't have any plans yet.</p>
										<Link
											to="exercise-type"
											className="btn btn-primary">
											Start your fitness journey
										</Link>
									</div>
								)}
							</>
						)
					) : (
						<div className="text-center">
							<LoadingComponent loading_text="Fetching exercise plans..." />
						</div>
					)}
				</div>
				<hr />
				<AverageOverViewComponent />
				<hr />
				<BodyWeightInfoComponent />
			</div>
		);
	}

	async getExercisePlans() {
		const token = await authService.getAccessToken();
		console.log("Bearer token:", token);
		await fetch("api/ApplicationUserExercisePlanModels/ExercisePlans", {
			headers: !token ? {} : { Authorization: `Bearer ${token}` },
		})
			.then(async (response) => {
				const { status } = response;
				if (status === 200) {
					return await response.json();
				} else if (status === 401) {
					throw new Error("Unauthorized. Login to continue");
				} else {
					throw new Error(
						"Unable to get your exercise plans. Something went wrong."
					);
				}
			})
			.then(async (data) => {
				await this.setState({
					exercisePlans: data,
				});
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise plans. Something went wrong";

				this.setState({
					errorMessage: error_message,
				});
				console.error(error);
				console.error(error.message);
			});

		this.setState({ loading: false });
	}

	async removeExercisePlans(exercisePlanId) {
		const token = await authService.getAccessToken();
		console.log("Bearer token:", token);
		await fetch(
			`api/ApplicationUserExercisePlanModels/RemoveExercisePlan/${exercisePlanId}`,
			{
				method: "DELETE",
				headers: !token ? {} : { Authorization: `Bearer ${token}` },
			}
		)
			.then(async () => {
				this.setState({
					exercisePlans: this.state.exercisePlans.filter(
						(exercisePlan) => exercisePlan.exercisePlanId != exercisePlanId
					),
				});
			})

			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to remove exercise plans. Something went wrong";

				// this.setState({
				// 	errorMessage: error_message,
				// });
				console.error(error);
				console.error(error.message);
			});

		//this.setState({ loading: false });
	}
}
