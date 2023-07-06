import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import LoadingComponent from "../../components/SharedComponents/LoadingComponent/LoadingComponent";
import ExerciseItemComponent from "../../components/ExerciseItemComponent/ExerciseItemComponent";

export default function ExerciseInfoPage() {
	const { exercise_id } = useParams();

	const [exercise, setExercise] = useState([]);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		getExerciseInfo(exercise_id);
	}, [exercise_id]);
	return (
		<div>
			<div>
				{loading === false ? (
					errorMessage ? (
						<div className="text-center text-danger">{errorMessage}</div>
					) : (
						<>
							<div className="row mt-3">
								<div className="col-xs-12 col-md-6 col-lg-4 col-xl-3 mt-3">
									<ExerciseItemComponent exercise={exercise} />
								</div>
								<div className="col-xs-12 col-md-6 col-lg-8 col-xl-9 mt-3 text-center">
									your progress graph will appear here
								</div>
							</div>
						</>
					)
				) : (
					<div className="text-center">
						<LoadingComponent loading_text="Loading exercises..." />
					</div>
				)}
			</div>
		</div>
	);

	async function getExerciseInfo(exercise_id) {
		await fetch(`api/ExerciseModels/${exercise_id}`)
			.then(async (response) => {
				return await response.json();
			})
			.then(async (data) => {
				await setExercise(data);
			})
			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to fetch exercise info. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}
