<?php

	header('Content-Type: text/html; charset=utf-8');

	if($_GET['mode'] == "crypt")
	{
		$crypts = new CaesarII($_GET['key']);
		echo $crypts->crypt($_GET['str']);
	}
	else
	{
		$crypts = new CaesarII($_GET['key']);
		echo $crypts->decrypt($_GET['str']);
	}	

	class CaesarII
	{
		private $text;
		private $key;
		private $alpha = array();

		const ALPHABET = "abcdefghijklmnopqrstuvwxyz";

		public function __construct($key)
		{
			$this->key = $key;
			$this->alpha = $this->createkeys($this->key);
		}

		private function createkeys($key)
		{
			$ret = array();
			foreach(str_split($key, 1) as $k)
				$ret[] = $this->simplekey($k);
			return $ret;
		}

		private function simplekey($k)
		{
			return str_split(substr(self::ALPHABET . self::ALPHABET, strpos(self::ALPHABET, $k), 26),1);
		}

		public function crypt($str)
		{
			$str = str_split(strtolower($str), 1);

			$keycount = count($this->alpha);

			$keynum = 0;

			foreach ($str as $p => $c) 
			{
				if($c == " " || $c == "," || $c == ".") continue;
				$str[$p] = $this->alpha[ $keynum%strlen($this->key) ][ strpos(self::ALPHABET, $c) ];
				$keynum++;
			}
			return implode($str);
		}

		public function decrypt($str)
		{
			$src = str_split(self::ALPHABET);
			$str = str_split(strtolower($str), 1);

			$keycount = count($this->alpha);

			$keynum = 0;

			foreach ($str as $p => $c) 
			{
				if($c == " " || $c == "," || $c == ".") continue;
				
				$str[$p] = $src[ strpos(implode($this->alpha[$keynum%4]), $c) ];
				$keynum++;
			}
			return implode($str);
		}

		public function keys()
		{
			$ret = "";
			foreach ($this->alpha as $key => $value) 
			{
				foreach ($value as $id => $char) 
				{
					$ret .= $char;
				}
				$ret .= "<br>";
			}
			return $ret;
		}
	}
