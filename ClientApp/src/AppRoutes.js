import ApiAuthorzationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";

import { Home } from "./components/Home";

// Pages
import ExerciseTypePage from "./pages/ExerciseType/ExerciseTypePage";

const AppRoutes = [
	{
		index: true,
		element: <Home />,
	},
	{
		path: "/exercise-type",
		requireAuth: false,
		element: <ExerciseTypePage />,
	},

	...ApiAuthorzationRoutes,
];

export default AppRoutes;
