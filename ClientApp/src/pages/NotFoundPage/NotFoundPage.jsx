import React from "react";
import { Link } from "react-router-dom";

export default function NotFoundPage() {
	return (
		<div className="min-vh-80 d-flex align-items-center justify-content-center">
			<div className="text-center">
				<p>Page not found.</p>
				<Link
					to="/"
					className="btn btn-primary">
					Return to home page
				</Link>
			</div>
		</div>
	);
}
