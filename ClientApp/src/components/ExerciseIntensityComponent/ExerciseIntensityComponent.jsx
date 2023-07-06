import React, { useState } from "react";

// Service
import authService from "../../components/api-authorization/AuthorizeService";
import MyButtonComponent from "../SharedComponents/MyButtonComponent/MyButtonComponent";

function ExerciseIntensityComponent(props) {
	const { exerciseId } = props;

	const [successMessage, setSuccessMessage] = useState(null);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(false);

	const [state, setState] = useState({
		weights: 1,
		repetitions: 1,
		exerciseDate: "",
	});

	const submitIntensityForm = (e) => {
		setLoading(true);
		e.preventDefault();

		const formData = {
			...state,
			ExerciseId: exerciseId,
		};

		if (state.exerciseDate === "") {
			formData.exerciseDate = null;
		}

		AddExerciseIntensity(formData);
	};

	const changeHandler = (e) => {
		const { id, value } = e.target;
		setState((prevState) => ({ ...prevState, [id]: value }));
	};

	return (
		<div className="p-3 shadow my-3 border">
			<form
				method="post"
				onSubmit={submitIntensityForm}>
				<div>
					<h3 className="text-center">Exercise intensity</h3>
				</div>
				<div className="my-3">
					<label htmlFor="weights">Weights</label>
					<input
						type="number"
						id="weights"
						inputMode="decimal"
						min={0}
						className="form-control"
						value={state.weights}
						onChange={changeHandler}
						required
					/>
				</div>
				<div className="my-3">
					<label htmlFor="repetitions">Repetitions</label>
					<input
						type="number"
						inputMode="numeric"
						id="repetitions"
						min={0}
						className="form-control"
						value={state.repetitions}
						onChange={changeHandler}
					/>
				</div>
				<div className="my-3">
					<label htmlFor="exerciseDate">Exercise date time</label>
					<input
						type="datetime-local"
						id="exerciseDate"
						className="form-control"
						value={state.exerciseDate}
						onChange={changeHandler}
					/>
				</div>
				<div className="my-3">
					<MyButtonComponent
						type="submit"
						className="form-control btn btn-primary"
						text="Add intensity"
						loading={loading}
					/>
				</div>

				{!loading ? (
					<>
						{errorMessage ? (
							<div>
								<p className="text-danger text-center">{errorMessage}</p>
							</div>
						) : (
							<>
								{successMessage ? (
									<div>
										<p className="text-success text-center">{successMessage}</p>
									</div>
								) : null}
							</>
						)}
					</>
				) : null}
			</form>
		</div>
	);

	async function AddExerciseIntensity(formData) {
		const token = await authService.getAccessToken();
		await fetch(`api/Intensitymodels`, {
			method: "POST",
			body: JSON.stringify(formData),
			headers: !token
				? {}
				: {
						Authorization: `Bearer ${token}`,
						"Content-type": "application/json; charset=UTF-8",
				  },
		})
			.then(async (response) => {
				const { status } = response;
				if (status === 200) {
					await setSuccessMessage("Intensity added");
				} else {
					throw new Error("Unable to add intensity. Something went wrong");
				}
			})

			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to add exercise plan. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}
export default ExerciseIntensityComponent;
