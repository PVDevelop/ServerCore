import React from 'react';
import { browserHistory } from 'react-router'

export default class RegisterForm extends React.Component
{
	constructor(props) 
	{
		super(props);
		this.state = 
		{
			email: '',
			password: ''
		};

		this.onEmailChange = this.onEmailChange.bind(this);
		this.onPasswordChange = this.onPasswordChange.bind(this);
		this.onButtonClicked = this.onButtonClicked.bind(this);
	}

	onEmailChange(event)
	{
		this.setState({email: event.target.value});
	}

	onPasswordChange(event)
	{
		this.setState({password: event.target.value});
	}
	
	onButtonClicked(e)
	{
		e.preventDefault();
		
		var data = {
			email: this.state.email,
			password: this.state.password
		};

		var json = JSON.stringify(data);

		var options =  {
			method: "post",
			headers: {
				"Accept": "application/json",
				"Content-Type": "application/json"
			},
			body: json
		};
		
		var url = "http://localhost:8000/api/users";

		fetch(url, options)
			.then(response => 
				{
					if(response.status == 200)
					{
						browserHistory.push('/confirm_sent');
					}
					else
					{
						alert(response.status);
					}
				})
			.catch(err => alert(err));
	}

	render()
	{
		return(
			<form>
				<div>
					<h2>Создание нового пользователя</h2>
				</div>

				<div>
					<label>E-mail</label>
					<input type="text" onChange={this.onEmailChange}/ >

					<label>Password</label>
					<input type="password" onChange={this.onPasswordChange}/ >
				</div>

				<div>
					<button onClick = {this.onButtonClicked}>Создать</button>
				</div>
			</form>
		);
	}
}