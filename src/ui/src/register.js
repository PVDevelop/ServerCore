import React from 'react';

export default class RegisterForm extends React.Component
{
	onButtonClicked()
	{
		alert("Допустим, пользователь создан!");
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
					<input type="text"/ >

					<label>Password</label>
				<input type="password"/ >
				</div>

				<div>
					<button onClick = {this.onButtonClicked}>Создать</button>
				</div>
			</form>
		);
	}
}