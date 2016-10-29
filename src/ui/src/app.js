import React from "react";
import ReactDOM from "react-dom";
import {Route, Router, Link, browserHistory} from "react-router";
import Home from "./home";
import RegisterFrom from "./register";
import ConfirmationSent from "./confirmation_sent";
import Confirm from "./confirmation";
import NotFound from "./not_found";

ReactDOM.render(
	<Router history={browserHistory}>
		<Route path='/' component={Home}/>
		<Route path='/register' component={RegisterFrom}/>
		<Route path='/confirm_sent' component={ConfirmationSent}/>
		<Route path='/confirmations/:confirmation_id' component={Confirm}/>

		<Route path='*' component={NotFound}/>
	</Router>,
	document.getElementById('main')
);