import React from "react";
import ReactDOM from "react-dom";
import {Route, Router, Link, browserHistory} from "react-router";
import Bar from "./bar";
import Home from "./home";
import LogonForm from "./logon";

ReactDOM.render(
	<Router history={browserHistory}>
		<Route path='/' component={Bar} />
		<Route path="logon" component ={LogonForm} />
	</Router>,
	document.getElementById('example')
);