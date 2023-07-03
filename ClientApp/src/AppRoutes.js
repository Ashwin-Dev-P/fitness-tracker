import ApiAuthorzationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";

import { Home } from "./components/Home";

// Pages
import ExerciseTypePage from "./pages/ExerciseType/ExerciseTypePage";
import ExercisePlansPage from "./pages/ExercisePlansPage/ExercisePlansPage";
import ExerciseDailyPlansPage from "./pages/ExerciseDailyPlansPage/ExerciseDailyPlansPage";

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
	{
		path: "/exercise-plans/:exercise_type_id",
		requireAuth: false,
		element: <ExercisePlansPage />,
	},
	{
		path: "/exercise-daily-plans/:exercise_plan_id/exercise-plan/:exercise_type_id",
		requireAuth: false,
		element: <ExerciseDailyPlansPage />,
	},

	...ApiAuthorzationRoutes,
];

export default AppRoutes;
