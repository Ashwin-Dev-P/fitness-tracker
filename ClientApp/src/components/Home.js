import React, { Component } from "react";

import authService from "./api-authorization/AuthorizeService";
import ExercisePlanItemComponent from "./ExercisePlanItemComponent/ExercisePlanItemComponent";
import LoadingComponent from "./SharedComponents/LoadingComponent/LoadingComponent";
import { Link } from "react-router-dom";

export class Home extends Component {
	constructor(props) {
		super(props);

		this.state = {
			exercisePlans: null,
			loading: true,
			errorMessage: null,
		};
	}
	componentDidMount() {
		this.getExercisePlans();
	}

	render() {
		var { loading, errorMessage, exercisePlans } = this.state;
		return (
			<div>
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
				return await response.json();
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
}
