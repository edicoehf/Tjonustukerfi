[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/React%20Vefur%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=8&branchName=master)
# Þjónustukerfi Edico React Web
Web interface for the service system created for Edico.

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.<br />

### `npm run build`

Builds the app for production to the `build` folder.<br />
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.<br />
Your app is ready to be deployed!

### `npm run serve`

Serves the builded app from the `build` folder.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

### `npm run docs`

Parse the jsdoc comments for a documentation visualizer to the `docs` folder. <br />
Open the `index.html` file in the `docs` folder in your browser to view the documentation.

## How to run
### Prerequisite
* Install Node.js & npm
* Install serve globally by running `npm install -g serve` in the project directory
* Install the required dependencies by running `npm install` in the project directory
### Run in development mode
* In the project directory run `npm start`
* The app will then be available at [http://localhost:3000](http://localhost:3000)
### Run in production mode
* Create an optimized build by running `npm run build` in the project directory
* In the project directory run `npm run serve`, this will serve the files from the /build
* The app will then be available at [http://localhost:3000](http://localhost:3000)
* To change the listening port, add `serve -s build -l {port}` as an argument, e.g. `serve -s build -l 3500`
* Refer to this section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

## Tests
The application is tested with Jest & Enzyme. Each component has a test file in it´s respective component folder with the same filename and a .test.js fileextension.
### How to run tests
* In the project directory run `npm test`
* This will launch the tester in an interactive watch mode and run tests automatically on saved changes.
* Pressing `w` will open up a menu of different actions for the tester, such as Running all tests, Running only the tests that failed in the last run, Run only tests that match a regular expression, etc.
* Refer to this section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

## Documentation
The application is documented with JSDoc comments which can be parsed and viewed in the browser without having to go through the source code. The documentation is categorized by purpose and subcategorized by components, hooks and services.
### How to view documentation
* In the project directory run `npm run docs`
* This will create a UI for viewing all the documentation in the folder `docs`.
* To view it open the file `index.html` inside the `docs` folder with your browser.

## UI Framework
Most of the interactive UI components are built using MaterialUI, for MaterialUI component API documentation and demos please refer to [https://material-ui.com](https://material-ui.com)
