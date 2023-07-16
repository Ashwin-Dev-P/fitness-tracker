import React, { useState } from "react";
import authService from "../api-authorization/AuthorizeService";
import MyButtonComponent from "../SharedComponents/MyButtonComponent/MyButtonComponent";

function CalorieFormComponent() {
	const [successMessage, setSuccessMessage] = useState(null);
	const [errorMessage, setErrorMessage] = useState(null);
	const [loading, setLoading] = useState(false);

	const [state, setState] = useState({
		CalorieCount: 0,

		CalorieConsumptionDate: "",
	});

	const submitBodyWeightForm = (e) => {
		setLoading(true);
		e.preventDefault();

		const formData = {
			...state,
		};

		if (state.CalorieConsumptionDate === "") {
			formData.CalorieConsumptionDate = null;
		}

		console.log(formData);
		AddCalorieData(formData);
	};

	const changeHandler = (e) => {
		const { id, value } = e.target;
		setState((prevState) => ({ ...prevState, [id]: value }));
	};

	return (
		<div className="p-3 card shadow border">
			<form
				method="post"
				onSubmit={submitBodyWeightForm}>
				<div>
					<h3 className="text-center">Enter your calorie intake</h3>
				</div>

				<>
					<div className="my-3">
						<label htmlFor="BodyWeight">Calorie count</label>
						<input
							type="number"
							id="CalorieCount"
							inputMode="number"
							min={0}
							step="any"
							className="form-control"
							value={state.weights}
							onChange={changeHandler}
							required
						/>
					</div>

					<div className="my-3">
						<label htmlFor="RecordedDate">Intake date</label>
						<input
							type="datetime-local"
							id="CalorieConsumptionDate"
							className="form-control"
							value={state.exerciseDate}
							onChange={changeHandler}
						/>
					</div>
					<div className="my-3">
						<MyButtonComponent
							type="submit"
							className="form-control btn btn-primary"
							text="Add calorie"
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
											<p className="text-success text-center">
												{successMessage}
											</p>
										</div>
									) : null}
								</>
							)}
						</>
					) : null}
				</>
			</form>
		</div>
	);

	async function AddCalorieData(formData) {
		const token = await authService.getAccessToken();
		await fetch(`api/CalorieModels`, {
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
					await setSuccessMessage("Calorie data added");
				} else {
					throw new Error("Unable to add calorie data. Something went wrong");
				}
			})

			.catch(async (error) => {
				const error_message =
					error && error.message
						? error.message
						: "Unable to add calorie data. Something went wrong";

				await setErrorMessage(error_message);
				console.error(error);
				console.error(error.message);
			});

		await setLoading(false);
	}
}

export default CalorieFormComponent;
