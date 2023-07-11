import ApiAuthorzationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";

import { Home } from "./components/Home";

// Pages
import ExerciseTypePage from "./pages/ExerciseType/ExerciseTypePage";
import ExercisePlansPage from "./pages/ExercisePlansPage/ExercisePlansPage";
import ExerciseDailyPlansPage from "./pages/ExerciseDailyPlansPage/ExerciseDailyPlansPage";
import ExercisesPage from "./pages/ExercisesPage/ExercisesPage";
import YourDailyPlansPage from "./pages/YourDailyPlansPage/YourDailyPlansPage";
import YourExercisesPage from "./pages/YourExercisesPage/YourExercisesPage";
import ExerciseInfoPage from "./pages/ExerciseInfoPage/ExerciseInfoPage";

const AppRoutes = [
	{
		index: true,
		requireAuth: true,
		path: "/",
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
	{
		path: "/exercises/exercise-id/:exercise_id",
		requireAuth: false,
		element: <ExerciseInfoPage />,
	},
	{
		path: "/exercises/exercise-daily-plan-id/:exercise_daily_plan_id/exercise-plan-id/:exercise_plan_id/exercise-type-id/:exercise_type_id",
		requireAuth: false,
		element: <ExercisesPage />,
	},
	{
		path: "/your-daily-plans/exercise-plan-id/:exercise_plan_id",
		requireAuth: true,
		element: <YourDailyPlansPage />,
	},
	{
		path: "/your-daily-plans/exercise-plan-id/:exercise_plan_id/exercise-daily-plan-id/:exercise_daily_plan_id",
		requireAuth: true,
		element: <YourExercisesPage />,
	},

	// Default page when no page is found
	{
		requireAuth: false,
		path: "*",
		element: <ExerciseTypePage />,
	},

	...ApiAuthorzationRoutes,
];

export default AppRoutes;
