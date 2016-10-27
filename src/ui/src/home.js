import React from "react";
import {Link} from "react-router";

export default class Home extends React.Component{
	render() {
		return(
			<nav>
				<Link to="/logon">Logon</Link>
			</nav>
		);
	}
}