import React from "react";
import ReactDOM from "react-dom";
import {Route, Router, Link, browserHistory} from "react-router";
import Home from "./home";
import RegisterFrom from "./register";

ReactDOM.render(
	<Router history={browserHistory}>
		<Route path='/' component={Home} />
		<Route path='register' component={RegisterFrom}/>
	</Router>,
	document.getElementById('example')
);