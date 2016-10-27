import React from 'react';

export default class LogonForm extends React.Component {
	onButtonClicked() {
		alert("clicked!");
	}

	render() {
		return( 
			<form>
				<label>login</label>
				<input type="text"/ >

				<label>password</label>
				<input type="password"/ >

				<button onClick = {this.onButtonClicked}>Submit</button>
			</form>);
	}
}