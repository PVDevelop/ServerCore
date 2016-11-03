import React from "react";
import ReactDOM from "react-dom";
import {Route, Router, Link, browserHistory} from "react-router";
import Home from "./components/home.jsx";
import RegisterFrom from "./components/register";
import Confirm from "./components/confirmation";
import NotFound from "./components/not_found";

ReactDOM.render(
	<Router history={browserHistory}>
		<Route path='/' component={Home}/>
		<Route path='/register' component={RegisterFrom}/>
		<Route path='/confirmations/:confirmation_id' component={Confirm}/>

		<Route path='*' component={NotFound}/>
	</Router>,
	document.getElementById('main')
);