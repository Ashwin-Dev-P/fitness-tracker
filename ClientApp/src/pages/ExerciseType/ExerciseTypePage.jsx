import React, { Component } from "react";

// Components
import ExerciseTypeItemComponent from "../../components/ExerciseTypeItemComponent/ExerciseTypeItemComponent";

// Shared components
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";
import { Link } from "react-router-dom";

// CSS
import "./ExerciseTypePage.css";
import BreadCrumbComponent from "../../components/SharedComponents/BreadCrumbComponent/BreadCrumbComponent";

class ExerciseTypePage extends Component {
	constructor(props) {
		super(props);

		this.state = {
			loading: true,
		};
	}
	componentDidMount() {
		this.getExerciseTypes();
	}

	render() {
		const { loading, error_message, exercise_types } = this.state;
		const crumbs = [
			{
				name: "Home",
				route: "/",
			},
			{
				name: "Exercise types",
				route: "/exercise-type",
			},
		];
		return (
			<>
				<BreadCrumbComponent crumbs={crumbs} />
				{loading === false ? (
					error_message ? (
						<div className="text-center text-danger">{error_message}</div>
					) : (
						<>
							{exercise_types && exercise_types.length > 0 ? (
								<>
									<h2 className="text-center">
										Select your desired training type
									</h2>
									<ul className="row">
										{exercise_types.map((exercise_type) => (
											<li
												key={exercise_type.exerciseTypeId}
												className="col-xs-12 col-md-6 col-lg-4 col-xl-3  my-4">
												<Link
													to={`/exercise-plans/${exercise_type.exerciseTypeId}`}>
													<ExerciseTypeItemComponent
														exercise_type={exercise_type}
													/>
												</Link>
											</li>
										))}
									</ul>
								</>
							) : (
								<div>
									<p>No exercise types available</p>
								</div>
							)}
						</>
					)
				) : (
					<div className="text-center">
						<LoadingComponent loading_text="Fetching exercise types..." />
					</div>
				)}
			</>
		);
	}

	async getExerciseTypes() {
		await fetch("api/exercisetypemodels")
			.then((response) => {
				return response.json();
			})
			.then((data) => {
				this.setState({
					exercise_types: data,
				});
			})
			.catch((error) => {
				this.setState({
					error_message:
						error && error.message
							? error.message
							: "Unable to fetch exercise types. Something went wrong",
				});

				console.error(error);
				console.error(error.message);
			});

		this.setState({
			loading: false,
		});
	}
}

export default ExerciseTypePage;
