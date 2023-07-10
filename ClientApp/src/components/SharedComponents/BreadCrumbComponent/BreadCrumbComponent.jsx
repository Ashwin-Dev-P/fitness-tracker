import React from "react";
import { Link } from "react-router-dom";

import "./BreadCrumbComponent.css";

export default function BreadCrumbComponent(props) {
	const { crumbs } = props;
	return (
		<nav
			aria-label="breadcrumb"
			className="breadcrumb-nav p-2 rounded shadow-sm">
			<ol className="breadcrumb mb-0">
				{crumbs.map(function (crumb, i) {
					if (i == crumbs.length - 1) {
						return (
							<li
								className="breadcrumb-item active"
								key={crumb.route}>
								{crumb.name}
							</li>
						);
					} else {
						return (
							<li
								className="breadcrumb-item "
								key={crumb.route}>
								<Link to={crumb.route}>{crumb.name}</Link>
							</li>
						);
					}
				})}
			</ol>
		</nav>
	);
}
