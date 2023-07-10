import React, { Component } from "react";
import { Modal, Button } from "react-bootstrap";

export default class ModalComponent extends Component {
	constructor(props) {
		super(props);

		const { show, modalMessageObj } = props;

		this.state = {
			show: show,
		};

		this.handleClose = this.handleClose.bind(this);
	}

	componentDidUpdate() {
		if (this.state.show != this.props.show) {
			const { show, modalMessageObj } = this.props;
			this.setState({
				show: show,
				modalMessageObj: modalMessageObj,
			});
		}
	}

	handleClose() {
		this.props.closeModal();
	}
	render() {
		const { show, modalMessageObj } = this.state;

		if (modalMessageObj) {
			var { modalBody, modalTitle, error } = modalMessageObj;
		}

		return (
			<>
				<Modal
					show={show}
					onHide={this.handleClose}>
					<Modal.Header closeButton>
						{modalTitle ? (
							<div className="text-center w-100">
								<Modal.Title>{modalTitle}</Modal.Title>
							</div>
						) : null}
					</Modal.Header>

					{modalBody ? (
						<Modal.Body>
							<p className={`text-center ${error ? "text-danger" : ""}`}>
								{modalBody}
							</p>
						</Modal.Body>
					) : null}

					<Modal.Footer>
						<Button
							variant="secondary"
							onClick={this.handleClose}>
							Close
						</Button>
					</Modal.Footer>
				</Modal>
			</>
		);
	}
}
